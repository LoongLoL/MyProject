// pages/signalr/signalr.js
///引入这个类库
const Hub = require('../../utils/signalr.js');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    msgs:[]
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.hubConnect = new Hub.HubConnection();

    this.hubConnect.start("https://localhost:5001/chathub", "ReceiveMessage");
    this.hubConnect.onOpen = res => {
      console.log("成功开启连接")
    }
    this.hubConnect.on("ReceiveMessage", (res1,res2) => {
      var that = this;
      var res = res1 + " says " + res2;
      console.log(res);
      var msgs = that.data.msgs;
      msgs.push(res);
      this.setData({
        msgs:msgs
      })
    })
  },

 
  
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }


})