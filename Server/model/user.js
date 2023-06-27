const mongoose = require('mongoose');

const userSchema = mongoose.Schema({
    name: {
        required: true,
        type: String
    },
    currency: {
        type: Number,
        default: 0
    },
    level: {
        type: Number,
        default: 0
    }
})

module.exports = mongoose.model('TestCluster', userSchema, 'User')