<template>
  <div class="about">
    <h1>Colors</h1>
    <p>You'd have to login to get here....</p>
    <ul>
      <li v-for="c in colors" :key="c">{{ c }}</li>
    </ul>
  </div>
</template>

<script>
  import store from "@/store";
  import { onMounted, computed } from "vue";
  export default {
    setup() {

      const colors = computed(() => store.state.colors);

      onMounted(() => store.dispatch("loadColors"));

      return {
        colors
      };
    }
  }
</script>
