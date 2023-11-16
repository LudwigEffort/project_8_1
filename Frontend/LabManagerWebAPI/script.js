function fetchItems() {
  const token = localStorage.getItem("authToken");

  fetch("http://localhost:5005/api/Item", {
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
