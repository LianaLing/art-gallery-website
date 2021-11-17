const path = require('path');
const glob = require('glob');
const { DefinePlugin } = require('webpack');
const { VueLoaderPlugin } = require('vue-loader');

module.exports = {
    mode: process.env.NODE_ENV === 'production' ? process.env.NODE_ENV : 'development',
    entry: glob.sync('./Scripts/src/**/*.{js,ts,vue}').reduce(function (obj, el) {
        obj[path.parse(el).name] = el;
        return obj
    }, {}),
    output: {
        publicPath: 'Scripts/dist',
        path: path.resolve(__dirname, './Scripts/dist'),
        filename: '[name].dist.js',
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
        extensions: ["*", ".js", ".ts", ".vue", ".json"],
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
            },
            {
                test: /\.tsx?$/,
                loader: 'ts-loader',
                exclude: /node_modules/,
                options: {
                    appendTsSuffixTo: [/\.vue$/],
                },
            }
        ],
    },
    plugins: [
        new DefinePlugin({
            __VUE_OPTIONS_API__: JSON.stringify(true),
            __VUE_PROD_DEVTOOLS__: JSON.stringify(false),
        }),
        new VueLoaderPlugin(),
    ]
};