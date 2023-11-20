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
const labAPI = new RestAPI("http://localhost:5005/LabManager/reservation/all");
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

  data.forEach(function (reservation) {
    var row = `<tr>
        <td>${reservation.id}</td>
        <td>${reservation.startTime}</td>
        <td>${reservation.endTime}</td>
        <td>${reservation.reservationStatus}</td>
        <td>${reservation.item.itemName}</td>
        <td>${reservation.labUser.firstName}</td>
        <td>${reservation.labUser.lastName}</td>
        <td><button class="btn btn-danger btn-sm" onclick="deleteItem(${reservation.id})">Delete</button></td>
    </tr>`;
    tableBody.innerHTML += row;
  });
}

//?? DELETE
async function deleteItem(id) {
  try {
    const restAPI = new RestAPI(
      "http://localhost:5005/LabManager/labUser/delete"
    );
    await restAPI.delete(id, token);
    console.log("Item delete");
  } catch (error) {
    console.error("Failed to delete item: " + error);
  }
}
