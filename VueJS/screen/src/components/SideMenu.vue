<template>
  <div class="side-menu-container">
    <div class="menu track">
        <img src="@/assets/icons/forward-button.png" alt="forward-button">
    </div>
    <div class="menu light">light</div>
    <div class="menu options">options</div>
  </div>
</template>

<script>
export default {
  name: 'SideMenu'
};
</script>

<style lang="css" scoped>
.side-menu-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background-color: #0067bb;
  min-width: 110px;
}

.menu img{
    width: 50px;
    height: 50px;
}
</style>