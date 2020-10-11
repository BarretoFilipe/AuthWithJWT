<template>
  <div class="hello">
    <h1>Olá {{ emailLogado }}</h1>
    <button @click="login()">Login</button>
    <button @click="getUsers()">Get Users</button>
    <div v-if="users.length > 0">
      <table style="width: 50%; text-align: left">
        <tr>
          <th>Id</th>
          <th>Nome</th>
          <th>Email</th>
          <th>Password</th>
        </tr>
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.id }}</td>
          <td>{{ user.name }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.password }}</td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "HelloWorld",
  data() {
    return {
      users: [],
      axiosInstance: axios.create({
        baseURL: "https://localhost:44377/v1/",
        timeout: 1000,
      }),
      token: "",
      emailLogado: "",
    };
  },
  methods: {
    login() {
      this.axiosInstance
        .post("account/login", {
          email: "barreto@email.com",
          password: "123456",
        })
        .then((result) => {
          this.emailLogado = result.data.email;
          this.token = result.data.token;
        })
        .catch((err) => {
          alert(err.response.data);
        });
    },

    getUsers() {
      this.axiosInstance.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${this.token}`;
      this.axiosInstance
        .get("user")
        .then((result) => {
          this.users = result.data;
        })
        .catch((err) => {
          alert(`Status - ${err.response.status}`);
        });
    },
  },
};
</script>
