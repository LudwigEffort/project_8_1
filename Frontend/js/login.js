//* DOM Scripts
const loginForm = document.getElementById("loginForm");

//* Backend Scripts
class RestAPI {
  baserURL;
  constructor(baserURL) {
    this.baserURL = baserURL;
  }
  async signIn(postJson) {
    try {
      const response = await fetch(`${this.baserURL}`, {
        method: "POST",
        headers: { "Content-type": "application/json; charset=UTF-8" },
        body: JSON.stringify(postJson),
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }

      const contentType = response.headers.get("content-type");

      if (!contentType || !contentType.includes("application/json")) {
        throw new TypeError("Received non-JSON response from server");
      }

      const data = await response.json();

      if (response.ok) {
        if (data.token) {
          localStorage.setItem("authToken", data.token);
        }
        return data;
      } else {
        throw new Error("HTTP error, state: " + response.status);
      }
    } catch (error) {
      console.error("Failed to login:", error);
    }
  }
}

const apiSignIn = new RestAPI("http://localhost:5224/auth/client/signIn");

loginForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  const email = document.getElementById("email").value;
  const password = document.getElementById("password").value;
  const loginRequest = {
    email: email,
    passwordWithNuance: password,
  };
  try {
    const result = await apiSignIn.signIn(loginRequest);
    console.log("Login successful:", result);
    //TODO: add redirect to lab manager
    window.location.href = "index.html";
  } catch (error) {
    console.error("Login failed:", error);
    //TODO: redirect to login
  }
});
