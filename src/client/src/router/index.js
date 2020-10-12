import { createRouter, createWebHashHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Colors from '../views/Colors.vue'
import Login from '../views/Login.vue'
import store from "@/store";

const authGuard = (to, from, next) => {
  if (store.getters.isAuthenticated) {
    next();
  } else {
    next("/login")
  }
};

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/colors',
    name: 'Colors',
    component: Colors,
    beforeEnter: authGuard
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

// Clear the error on every navigation
router.afterEach(() => {
  store.commit("clearError");
});


export default router
