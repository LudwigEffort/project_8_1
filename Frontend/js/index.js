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
const testBtn = document.getElementById("myButton");

testBtn.addEventListener("click", async (event) => {
  try {
    const result = await labAPI.get(token);
    console.log(token);
    console.log(result);
  } catch (error) {
    console.error("Failed to get: " + error);
    console.log(token);
  }
});
