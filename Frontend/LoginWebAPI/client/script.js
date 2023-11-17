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
  async signUp(postJson) {
    try {
      const response = await fetch(`${this.baserURL}`, {
        method: "POST",
        headers: { "Content-type": "application/json; charset=UTF-8" },
        body: JSON.stringify(postJson),
      });

      const data = await response.json();

      if (response.ok) {
        return data;
      } else {
        throw new Error("HTTP error, state: " + response.status);
      }
    } catch (error) {
      console.error("Failed to sign up:", error);
    }
  }
}

const apiSignIn = new RestAPI("http://localhost:5224/auth/client/signIn");
const loginForm = document.getElementById("formLogin");

loginForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  const email = document.getElementById("loginEmail").value;
  const password = document.getElementById("loginPassword").value;
  const loginRequest = {
    email: email,
    passwordWithNuance: password,
  };
  try {
    const result = await apiSignIn.signIn(loginRequest);
    console.log("Login successful:", result);
    //TODO: add redirect to lab manager
  } catch (error) {
    console.error("Login failed:", error);
    //TODO: redirect to login
  }
});

const apiSignUp = new RestAPI("http://localhost:5224/auth/client/signUp");
const signUpForm = document.getElementById("formSignup");

signUpForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  const email = document.getElementById("signupEmail").value;
  const password = document.getElementById("signupPassword").value;
  const firstName = document.getElementById("signupFirstName").value;
  const lastName = document.getElementById("signupLastName").value;
  const phone = document.getElementById("signupPhone").value;
  const signUpRequest = {
    emailAddress: email,
    password: password,
    firstName: firstName,
    lastName: lastName,
    phoneNumber: phone,
  };
  try {
    const result = await apiSignUp.signUp(signUpRequest);
    console.log("Login successful: ", result);
    //TODO: redirect to login
  } catch (error) {
    console.error("Sign up failed: ", error);
    //TODO: redirect to signup
  }
});
