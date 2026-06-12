const restUri = "http://localhost:8025/api/manager";

class ManagerClient {

    constructor() {
        this.manager = { id: "", passcode: "", token: "" };
        this.status = "";
    }

    async requestOtp() {
        let response = await fetch(`${restUri}/signin?id=${this.manager.id}&passcode=${this.manager.passcode}`);

        if (response.ok) {
            this.status = "OTP sent to your email!";
            alert(this.status);
        } else {
            this.status = "Invalid manager credentials!";
            alert(this.status);
        }
    }

    async verifyOtp() {
        let response = await fetch(`${restUri}/signin?id=${this.manager.id}&passcode=${this.manager.passcode}`);

        if (response.ok) {
            this.manager.token = await response.text();
            this.status = "Login successful!";
            alert(this.status);
        } else {
            this.status = "Incorrect OTP!";
            alert(this.status);
        }
    }

    async getAllEmployees() {
        let request = {
            method: "get",
            headers: {
                "Authorization": "Bearer " + this.manager.token
            }
        };

        let response = await fetch("http://localhost:5190/api/emp/", request);

        if (response.ok) {
            let employees = await response.json();
            console.log("Employees:", employees);
            this.status = "";
            return employees;
        } else {
            this.status = "Unauthorized or failed!";
            alert(this.status);
            return [];
        }
    }
}
