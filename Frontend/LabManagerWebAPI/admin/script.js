//* DOM scripts
//? Script for select tag
document
  .getElementById("httpMethodSelect")
  .addEventListener("change", function () {
    let method = this.value;
    let contents = document.querySelectorAll(".method-content");
    contents.forEach(function (content) {
      content.style.display = "none";
    });
    document.getElementById(method).style.display = "block";
  });

//? Script for print JSON to table
function createTableFromJson(jsonData) {
  var data = JSON.parse(jsonData);

  var table = document.createElement("table");
  table.className = "table table-striped";

  var thead = table.createTHead();
  var row = thead.insertRow();

  Object.keys(data[0]).forEach(function (key) {
    var th = document.createElement("th");
    th.scope = "col";
    th.innerHTML = key;
    row.appendChild(th);
  });

  var tbody = table.createTBody();

  data.forEach(function (item) {
    var row = tbody.insertRow();
    Object.keys(item).forEach(function (key) {
      var cell = row.insertCell();
      cell.innerHTML = item[key];
    });
  });
  document.getElementById("tableContainer").appendChild(table);
}

//* Backend scripts
class RestLabApi {
  baseUrl;
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
  }
  async getItems() {
    try {
      const token = localStorage.getItem("authToken");
      const response = await fetch(`${this.baseUrl}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-type": "application/json; charset=UTF-8",
        },
      });
      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }
      return await response.json();
    } catch (error) {
      console.error("Failed to get all items: " + error);
    }
  }
}

//? Script for auth...
const api = new RestLabApi("http://localhost:5005/api/item");

document.getElementById("printGET").addEventListener("click", function () {
  api.getItems().then((data) => {
    if (data) {
      createTableFromJson(JSON.stringify(data));
    }
  });
});
