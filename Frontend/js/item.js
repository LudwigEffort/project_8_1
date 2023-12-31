//* Backend scripts
class RestAPI {
  baseURL;
  constructor(baseURL) {
    this.baseURL = baseURL;
  }
  //? GET all
  async get(token) {
    try {
      const response = await fetch(`${this.baseURL}`, {
        method: "GET",
        headers: { Authorization: `Bearer ${token}` },
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }
      return await response.json();
    } catch (error) {
      console.error("Failed to GET: " + error);
    }
  }
  //? Get by Id
  async getById(id, token) {
    try {
      const response = await fetch(`${this.baseURL}/${id}`, {
        method: "GET",
        headers: { Authorization: `Bearer ${token}` },
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }
      return await response.json();
    } catch (error) {
      console.error("Failed to GET: " + error);
    }
  }
  //? POST
  async create(postJson, token) {
    try {
      const response = await fetch(`${this.baseURL}`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-type": "application/json; charset=UTF-8",
        },
        body: JSON.stringify(postJson),
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }

      return await response.json();
    } catch (error) {
      console.log("Failed to POST: " + error);
    }
  }
  //? PUT
  async update(id, putJson, token) {
    try {
      const response = await fetch(`${this.baseURL}/edit/${id}`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-type": "application/json; charset=UTF-8",
        },
        body: JSON.stringify(putJson),
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }

      if (response.status === 204) {
        console.log("Update data successful");
      }
    } catch (error) {
      console.log("Failed to PUT: " + error);
    }
  }
  //? DELETE
  async delete(id, token) {
    try {
      const response = await fetch(`${this.baseURL}/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }
      if (response.status === 204) {
        console.log("Delete data successful");
      }
      // return await response.json();
    } catch (error) {
      console.error("Failed to DELETE: " + error);
    }
  }
}

//* DOM scripts
const token = localStorage.getItem("authToken");

//?? GET
const labAPI = new RestAPI("http://localhost:5005/LabManager/item/all");
const getBtn = document.getElementById("getBtn");

getBtn.addEventListener("click", async () => {
  try {
    const result = await labAPI.get(token);
    console.log(token);
    generateTable(result);
  } catch (error) {
    console.error("Failed to GET request: " + error);
    console.log(token);
  }
});

//? Generate Table of Items
function generateTable(data) {
  var tableBody = document.getElementById("tableBody");
  tableBody.innerHTML = "";
  data.forEach(function (item) {
    var row = `<tr>
        <td>${item.id}</td>
        <td>${item.itemName}</td>
        <td>${item.itemType}</td>
        <td>${item.description}</td>
        <td>${item.techSpec}</td>
        <td>${item.itemIdentifier}</td>
        <td>${item.status}</td>
        <td>${new Date(item.creationDate).toLocaleDateString()}</td>
        <td>${item.roomId}</td>
        <td>${
          item.softwares.map((software) => software.softwareName).join(", ") ||
          "Nessun Software"
        }</td>
        <td><button class="btn btn-primary btn-sm" onclick="editItem(${
          item.id
        })">Edit</button></td>
        <td><button class="btn btn-danger btn-sm" onclick="deleteItem(${
          item.id
        })">Delite</button></td>
    </tr>`;
    tableBody.innerHTML += row;
  });
}

//?? POST
const createForm = document.getElementById("createForm");
const createItemButton = document.getElementById("addItemBtn");

createForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const softwareIds = document
      .getElementById("createSoftwares")
      .value.split(",")
      .map((idStr) => idStr.trim())
      .filter((idStr) => idStr.length > 0)
      .map((trimmedIdStr) => parseInt(trimmedIdStr));

    const softwareData = softwareIds.map((id) => {
      return { id, softwareName: "string" };
    });

    const itemData = {
      itemName: document.getElementById("createName").value,
      itemType: document.getElementById("createType").value,
      description: document.getElementById("createDescription").value,
      techSpec: document.getElementById("createTechSpec").value,
      itemIdentifier: document.getElementById("createItemIdentifier").value,
      status: document.getElementById("createStatus").value,
      roomId: parseInt(document.getElementById("createRoomId").value, 10),
      softwares: softwareData,
    };
    const restAPI = new RestAPI("http://localhost:5005/LabManager/item/create");
    const result = await restAPI.create(itemData, token);
    console.log(result);
  } catch (error) {
    console.error("Failed to create data: " + error);
  }
});

createItemButton.addEventListener("click", () => {
  document.getElementById("createContainer").style.display = "block";
});

//?? PUT
async function loadItemById(id, token) {
  try {
    const restAPI = new RestAPI("http://localhost:5005/LabManager/item");
    const data = await restAPI.getById(id, token);
    document.getElementById("editId").value = data.id;
    document.getElementById("editName").value = data.itemName;
    document.getElementById("editType").value = data.itemType;
    document.getElementById("editDescription").value = data.description;
    document.getElementById("editTechSpec").value = data.techSpec;
    document.getElementById("editItemIdentifier").value = data.itemIdentifier;
    document.getElementById("editStatus").value = data.status;
    document.getElementById("editRoomId").value = data.roomId;
    document.getElementById("editSoftwares").value = data.softwares
      .map((s) => s.id)
      .join(", ");
  } catch (error) {
    console.error("Failed to load data: " + error);
  }
}

const updateForm = document.getElementById("editForm");

updateForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const itemId = document.getElementById("editId").value;

    const softwareIds = document
      .getElementById("editSoftwares")
      .value.split(",")
      .map((idStr) => idStr.trim())
      .filter((idStr) => idStr.length > 0)
      .map((trimmedIdStr) => parseInt(trimmedIdStr));

    const softwareData = softwareIds.map((id) => {
      return { id, softwareName: "string" };
    });

    const itemData = {
      id: parseInt(itemId, 10),
      itemName: document.getElementById("editName").value,
      itemType: document.getElementById("editType").value,
      description: document.getElementById("editDescription").value,
      techSpec: document.getElementById("editTechSpec").value,
      itemIdentifier: document.getElementById("editItemIdentifier").value,
      status: document.getElementById("editStatus").value,
      roomId: parseInt(document.getElementById("editRoomId").value, 10),
      softwares: softwareData,
    };

    const restAPI = new RestAPI("http://localhost:5005/LabManager/item");

    const reusult = await restAPI.update(itemId, itemData, token);
    console.log(reusult);
  } catch (error) {
    console.error("Failed to update data: " + error);
  }
});

function editItem(id) {
  loadItemById(id, token).then(() => {
    document.getElementById("editContainer").style.display = "block";
  });
}

//?? DELETE
async function deleteItem(id) {
  try {
    const restAPI = new RestAPI("http://localhost:5005/LabManager/item/delete");
    await restAPI.delete(id, token);
    console.log("Item delete");
  } catch (error) {
    console.error("Failed to delete item: " + error);
  }
}
