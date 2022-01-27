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
  background-color: #0095ff;
  padding: 20px 20px;
  width: 10%;
  min-width: 190px;
  overflow-y: scroll;
}

.side-menu-container::-webkit-scrollbar{
  width: 10px;
}

.side-menu-container::-webkit-scrollbar-thumb{
  background-color: #ffffff94;
  border-radius: 10px;
  height: 8px;
}

.menu img{
    width: 50px;
    height: 50px;
}
</style>