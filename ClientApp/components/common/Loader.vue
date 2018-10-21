<template>
  <transition name="modal">
    <div class="modal-mask modal-center">
      <div class="lds-roller">
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
      </div>
    </div>
  </transition>
</template>

<style lang="scss" scoped>
.modal-center {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
