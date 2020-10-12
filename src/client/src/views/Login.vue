<template>
  <div>
    <h1>Login</h1>
    <form novalidate @submit.prevent="onSubmit()">
      <div class="form-group">
        <label for="username">Username</label>
        <input type="text" name="username" v-model="model.username" class="form-control" />
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <input type="password" name="password" v-model="model.password" class="form-control" />
      </div>
      <div class="form-group">
        <input type="submit" class="btn btn-success" value="Login" />&nbsp;
        <router-link class="btn btn-info" to="/">Cancel</router-link>
      </div>
    </form>
  </div>
</template>

<script>
  import { reactive } from "vue";
  import store from "@/store";

  export default {
    setup() {

      const model = reactive({ username: "", password: ""});

      function onSubmit() {
        store.dispatch("login", model);
      }

      return {
        model,
        onSubmit
      }
    }
  }
</script>