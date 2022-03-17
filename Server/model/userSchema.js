const mongoose = require('mongoose');
const { Schema } = mongoose;


const userSchema = new Schema({
  name: {
    type: String,
    required: true,
  },
  username: {
      type: String,
      required: true,
  },
  password: {
      type: String,
      required: true,
  },
  status: {
    type: String,
    enum: ["active", "inactive"],
  }
});
mongoose.model('User', userSchema);

//module.exports = userSchema;