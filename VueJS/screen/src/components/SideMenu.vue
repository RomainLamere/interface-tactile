<template>
  <div class="side-menu-container">
    <the-table></the-table>
  </div>
</template>

<script>
import TheTable from './TheTable.vue';
export default {
  components: { TheTable },
  name: 'SideMenu'
};
</script>

<style lang="css" scoped>

.side-menu-container{
  background-color: #0067bb;
  padding: 20px 20px;
  min-width: 200px;
}

.menu img{
    width: 50px;
    height: 50px;
}
</style>