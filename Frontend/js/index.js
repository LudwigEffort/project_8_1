//* Backend scripts
class RestAPI {
  baseURL;
  constructor(baseURL) {
    this.baseURL = baseURL;
  }
  async get(token) {
    try {
      const response = await fetch(`${this.baseURL}`, {
        method: "GET",
        headers: { Authorization: `Bearer ${token}` },
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }
      const data = await response.json();

      if (response.ok) {
        return data;
      } else {
        throw new Error("HTTP error, state: " + response.status);
      }
    } catch (error) {
      console.error("Failed to GET " + error);
    }
  }
}

//* DOM scripts
const token = localStorage.getItem("authToken");

//? test GET
const labAPI = new RestAPI("http://localhost:5005/api/Item");
const getBtn = document.getElementById("getBtn");

getBtn.addEventListener("click", async (event) => {
  try {
    const result = await labAPI.get(token);
    console.log(token);
    console.log(result);
    generateTable(result);
  } catch (error) {
    console.error("Failed to get: " + error);
    console.log(token);
  }
});

function generateTable(data) {
  var tableBody = document.getElementById("tableBody");
  tableBody.innerHTML = ""; // Pulisce la tabella prima di popolarla
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
