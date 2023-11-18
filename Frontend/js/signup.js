//* Backend Scripts
class RestAPI {
  baseURL;
  constructor(baseURL) {
    this.baseURL = baseURL;
  }
  async signUp(postJson) {
    try {
      const response = await fetch(`${this.baseURL}`, {
        method: "POST",
        headers: { "Content-type": "application/json; charset=UTF-8" },
        body: JSON.stringify(postJson),
      });

      if (!response.ok) {
        throw new Error("HTTP error, state: " + response.status);
      }

      const contentType = response.headers.get("content-type");

      //? this why when the server return a non json response (!= Ok(new { message = "Successfully created" }) in ASP.NET)
      if (!contentType || !contentType.includes("application/json")) {
        throw new TypeError("Received non-JSON response from server");
      }

      const data = await response.json();

      if (response.ok) {
        return data;
      } else {
        throw new Error("HTTP error, state: " + response.status);
      }
    } catch (error) {
      console.error("Failed to SignUp: " + error);
    }
  }
}

//* DOM Scripts
const signUpForm = document.getElementById("signUpForm");

//? SignUp Form
const loginAPI = new RestAPI("http://localhost:5224/auth/client/signUp");

signUpForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  const email = document.getElementById("email").value;
  const password = document.getElementById("password").value;
  const firstName = document.getElementById("firstName").value;
  const lastName = document.getElementById("lastName").value;
  const phone = document.getElementById("phone").value;
  const signUpRequest = {
    emailAddress: email,
    password: password,
    firstName: firstName,
    lastName: lastName,
    phoneNumber: phone,
  };
  try {
    const result = await loginAPI.signUp(signUpRequest);
    console.log("SignUp successful: " + result);
    window.location.href = "login.html";
  } catch (error) {
    console.error("SignUp failed: " + error);
  }
});
