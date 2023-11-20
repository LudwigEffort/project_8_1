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
const loginAPI = new RestAPI(
  "http://localhost:5224/LoginManager/admin/user/all"
);
const getBtn = document.getElementById("getBtn");

getBtn.addEventListener("click", async () => {
  try {
    const result = await loginAPI.get(token);
    console.log(token);
    generateTable(result);
  } catch (error) {
    console.error("Failed to GET request: " + error);
    console.log(token);
  }
});

//? Generate Table
function generateTable(data) {
  var tableBody = document.getElementById("tableBody");
  tableBody.innerHTML = "";
  data.forEach(function (item) {
    var row = `<tr>
        <td>${item.id}</td>
        <td>${item.emailAddress}</td>
        <td>${item.role}</td>
        <td>${item.firstName}</td>
        <td>${item.lastName}</td>
        <td>${item.phoneNumber}</td>
        <td>${item.isBanned ? "Yes" : "No"}</td>
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

//?? PUT
async function loadItemById(id, token) {
  try {
    const restAPI = new RestAPI(
      "http://localhost:5224/LoginManager/admin/user/edit"
    );
    const data = await restAPI.getById(id, token);
    document.getElementById("editId").value = data.id;
    document.getElementById("editEmail").value = data.emailAddress;
    document.getElementById("editRole").value = data.role;
    document.getElementById("editFirstName").value = data.firstName;
    document.getElementById("editLastName").value = data.lastName;
    document.getElementById("editPhone").value = data.phoneNumber;
    document.getElementById("editIsBanned").value = data.isBanned.toString();
  } catch (error) {
    console.error("Failed to load data: " + error);
  }
}

const updateForm = document.getElementById("editForm");

updateForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const userId = document.getElementById("editId").value;

    const userData = {
      id: parseInt(userId, 10),
      emailAddress: document.getElementById("editEmail").value,
      role: document.getElementById("editRole").value,
      firstName: document.getElementById("editFirstName").value,
      lastName: document.getElementById("editLastName").value,
      phoneNumber: document.getElementById("editPhone").value,
      isBanned: document.getElementById("editIsBanned").value === "true",
    };

    const restAPI = new RestAPI(
      "http://localhost:5224/LoginManager/admin/user/edit"
    );

    const result = await restAPI.update(userId, userData, token);
    console.log(result);
  } catch (error) {
    console.error("Failed to update data: " + error);
  }
});

function editItem(id) {
  loadItemById(id, token).then(() => {
    document.getElementById("editContainer").style.display = "block";
  });
}
