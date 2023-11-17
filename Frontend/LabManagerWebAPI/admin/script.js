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

function fetchItems() {
  const token = localStorage.getItem("authToken");

  fetch("https://your-lab-api.com/items", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => console.error("Error:", error));
}
