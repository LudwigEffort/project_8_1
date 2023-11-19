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
const labAPI = new RestAPI("http://localhost:5005/LabManager/labUser/all");
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

function generateTable(data) {
  var tableBody = document.getElementById("tableBody");
  tableBody.innerHTML = "";

  data.forEach(function (item) {
    var row = `<tr>
        <td>${item.id}</td>
        <td>${item.firstName}</td>
        <td>${item.lastName}</td>
        <td>${item.emailAddress}</td>
        <td>${item.phoneNumber}</td>
        <td>${item.role}</td>
        <td><button class="btn btn-primary btn-sm" onclick="editItem(${item.id})">Edit</button></td>
        <td><button class="btn btn-danger btn-sm" onclick="deleteItem(${item.id})">Delete</button></td>
    </tr>`;
    tableBody.innerHTML += row;
  });
}

//?? POST
const createForm = document.getElementById("createForm");
const createItemButton = document.getElementById("addBtn");

createForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const itemData = {
      id: parseInt(0, 10),
      firstName: document.getElementById("createFirstName").value,
      lastName: document.getElementById("createLastName").value,
      emailAddress: document.getElementById("createEmail").value,
      phoneNumber: document.getElementById("createPhone").value,
      role: document.getElementById("createRole").value,
      token: document.getElementById("createToken").value,
    };
    const restAPI = new RestAPI(
      "http://localhost:5005/LabManager/labUser/create"
    );
    const result = await restAPI.create(itemData, token);
    console.log(result);
  } catch (error) {
    console.error("Failed to create data: " + error);
  }
});

createItemButton.addEventListener("click", () => {
  document.getElementById("createContainer").style.display = "block";
});