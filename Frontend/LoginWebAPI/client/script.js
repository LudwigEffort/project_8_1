class RestAPI {
  baserURL;
  constructor(baserURL) {
    this.baserURL = baserURL;
  }
  async login(postJson) {
    try {
      const response = await fetch(`${this.baserURL}`, {
        method: "POST",
        headers: { "Content-type": "application/json; charset=UTF-8" },
        body: JSON.stringify(postJson),
      });

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

const restAPI = new RestAPI(
  "http://localhost:5224/api/LoginClientService/login"
);

let user = { email: "client1@example.com", passwordWithNuance: "client123" };

//let test = restAPI.login(user);

//console.log(test);

document.addEventListener("DOMContentLoaded", () => {
  const loginButton = document.getElementById("toggleLogin");
  const signupButton = document.getElementById("toggleSignup");
  const loginForm = document.getElementById("loginForm");
  const signupForm = document.getElementById("signupForm");

  loginButton.addEventListener("click", () => {
    loginForm.style.display = "block";
    signupForm.style.display = "none";

    loginButton.classList.add("btn-primary");
    loginButton.classList.remove("btn-secondary");

    signupButton.classList.add("btn-secondary");
    signupButton.classList.remove("btn-primary");
  });

  signupButton.addEventListener("click", () => {
    signupForm.style.display = "block";
    loginForm.style.display = "none";

    signupButton.classList.add("btn-primary");
    signupButton.classList.remove("btn-secondary");

    loginButton.classList.add("btn-secondary");
    loginButton.classList.remove("btn-primary");
  });
});
