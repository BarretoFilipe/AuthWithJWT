<template>
  <div class="hello">
    <h1>Olá {{ emailLogado }}</h1>
    <button @click="login()">Login</button>
    <button @click="getUsers()">Get Users</button>
    <div id="snackbar">JWT copiado para o ctrl+c</div>
    <div v-if="tokenheaderDecoded.length > 0">
      <p>HEADER:ALGORITHM & TOKEN TYPE</p>
      <p> {{ tokenheaderDecoded }} </p>
      <p>PAYLOAD:DATA</p>
      <p> {{ tokenDecoded }} </p>    
    </div>
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
import jwtDecode from "jwt-decode";

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
      tokenheaderDecoded:"",
      tokenDecoded:"",
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
          this.tokenheaderDecoded = JSON.stringify(jwtDecode(this.token, { header: true }), null, 4);
          this.tokenDecoded = JSON.stringify(jwtDecode(this.token), null, 4);

          navigator.clipboard.writeText(this.token);
          this.snackbarShow();
        })
        .catch((err) => {
          alert(err.response.data);
        });
    },

    snackbarShow(){
      const snackbar = document.getElementById("snackbar");
      snackbar.className = "show";
      setTimeout(function(){ snackbar.className = snackbar.className.replace("show", ""); }, 3000);
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

<style>
p{
  white-space: pre-wrap;
  text-align: left;
}
#snackbar {
  visibility: hidden;
  min-width: 250px;
  margin-left: -125px;
  background-color: #333;
  color: #fff;
  text-align: center;
  border-radius: 2px;
  padding: 16px;
  position: fixed;
  z-index: 1;
  left: 50%;
  bottom: 30px;
  font-size: 17px;
}

#snackbar.show {
  visibility: visible;
  -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
  animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

@-webkit-keyframes fadein {
  from {bottom: 0; opacity: 0;} 
  to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
  from {bottom: 0; opacity: 0;}
  to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
  from {bottom: 30px; opacity: 1;} 
  to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
  from {bottom: 30px; opacity: 1;}
  to {bottom: 0; opacity: 0;}
}
</style>