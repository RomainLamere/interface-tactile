import Workspace from './components/Workspace.vue'
import VueRouter from 'vue-router'


const routes = [
    { path: '/' , component: Workspace}
]

export default new VueRouter({
    routes,
    mode: 'history'
})