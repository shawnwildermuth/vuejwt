<template>
  <div class="row">
    <div class="col-sm-8 offset-sm-2">
      <div id="nav">
        <router-link to="/">Home</router-link> |
        <router-link to="/colors">Colors</router-link> 
        <span v-if="!isAuthenticated">
          |
          <router-link to="/login">Login</router-link> 
        </span>
        <span v-if="isAuthenticated">
          |           
          <a href="#" @click.prevent="onLogout">Logout</a>
        </span>
      </div>
      <div class="alert alert-info" v-if="isBusy">Loading...</div>
      <div class="alert alert-danger" v-if="error">{{ error }}</div>
      <router-view />
    </div>
  </div>
</template>


<script>
  import store from "@/store";
  import { computed } from "vue";

  export default {
    setup() {
      return {
        isBusy: computed(() => store.state.isBusy),
        error: computed(() => store.state.error),
        isAuthenticated: computed(() => store.getters.isAuthenticated),
        onLogout: () => store.dispatch("logout")
      }
    }
  }
</script>