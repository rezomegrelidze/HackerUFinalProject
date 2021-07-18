import React, { Component } from 'react';

export class Login extends React.Component {

    constructor(props) {
        super(props);

        this.state = { username: "", password: "", role: "" };

        this.handleChange = this.handleChange.bind(this);
        console.log(this.state);
    }

    async login() {
        try {
            const response =
                await fetch(
                    `login?username=${this.state.username}&password=${this.state.password}&role=${this.state.role}`,
                    { method: "GET" });
            localStorage.setItem("token", response.text());
        } catch (ex) {
            console.log(ex);
        }
    }

    handleChange(event) {
        this.setState({ [event.target.name]: event.target.value });
    }

    render() {
        return (
            <form onSubmit={this.login}>
                <div className="form-group">
                    <label>Username: </label>
                    <input className="form-control" type="text" value={this.state.username} onChange={this.handleChange}
                            name="username"/>
                </div>
                <div className="form-group">
                    <label>Password: </label>
                    <input className="form-control" type="password" value={this.state.password} onChange={this.handleChange}
                            name="password"/>
                </div>
                <div className="form-group">
                    <label>Role: </label>
                    <select className="form-control" name="role" value={this.state.role} onChange={this.handleChange}>
                        <option value="Administrator">Administrator</option>
                        <option value="Customer">Customer</option>
                        <option value="AirlineCompany">AirlineCompany</option>
                    </select>
                </div>
                <div className="form-group">
                    <input className="btn btn-primary" type="submit" value="Log In" />
                </div>
            </form>
        );
    }
}