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
    const reservationData = {
      startTime: document.getElementById("startTime").value,
      itemId: parseInt(document.getElementById("itemId").value),
      labUserId: parseInt(document.getElementById("labUserId").value),
    };
    const restAPI = new RestAPI(
      "http://localhost:5005/LabManager/reservation/create"
    );
    const result = await restAPI.create(reservationData, token);
    console.log(result);
  } catch (error) {
    console.error("Failed to create reservation: ", error);
  }
});

createItemButton.addEventListener("click", () => {
  document.getElementById("createContainer").style.display = "block";
});
