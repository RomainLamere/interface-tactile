<template>
  <div class="alert" :class="{'open': zone, 'close': !zone}">
    <div class="color" :class="zone"></div>
    <div class="text">New record</div>
  </div>
</template>
<script>
export default {
  name: "Alert",
  props: {
    zone: String,
  },
};
</script>
<style lang="css" scoped>
  .alert {
    font-family: sans-serif;
    color: white;
    display: flex;
    position: fixed;
    top: 40px;
    left: -206px;
    background-color: #535353;
    height: 50px;
    width: 200px;
    border-radius: 6px;
    overflow: hidden;
    opacity: 0.5;
    transition: transform 1s ease, opacity 1s ease;
  }

  .open{
    transform: translateX(211px);
    opacity: 1;
  }

  .close{
    transform: translateX(-211px);
    opacity: 0.5;
  }

  .color{
    width: 10px;
    height: 100%;
  }

  .text{
    display: flex;
    align-items: center;
    padding: 0.5em;
  }

  .A {
    background-color: #d66f6f;
  }

  .B {
    background-color: #6fd674;
  }

  .C {
    background-color: #6f87d6;
  }

  .D {
    background-color: #d4d66f;
  }
</style>
