const path = require('path');
const glob = require('glob');
const { DefinePlugin } = require('webpack');

module.exports = {
    mode: process.env.NODE_ENV === 'production' ? process.env.NODE_ENV : 'development',
    entry: glob.sync('./Scripts/src/**/*.js').reduce(function(obj, el){
       obj[path.parse(el).name] = el;
       return obj
    },{}),
    output: {
        publicPath: 'Scripts/dist',
        path: path.resolve(__dirname, './Scripts/dist'),
        filename: '[name].dist.js',
        libraryTarget: 'commonjs2',
    },
    // resolve: {
    //     alias: {
    //         'vue': '../../node_modules/vue/dist/vue.esm-bundler.js'
    //     }
    // },
    resolve: {
        alias: {
            vue$: "vue/dist/vue.esm-bundler.js",
        },
        extensions: ["*", ".js", ".vue", ".json"],
    },
    plugins: [
        new DefinePlugin({
            __VUE_OPTIONS_API__: JSON.stringify(true),
            __VUE_PROD_DEVTOOLS__: JSON.stringify(false),
        })
    ]
};