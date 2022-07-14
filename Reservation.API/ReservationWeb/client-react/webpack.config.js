const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    // mode: 'development',

    entry: {
        index: path.resolve(__dirname, './src/index.tsx'),
    },
    output: {
        path: path.resolve(__dirname, './dist'),
        // path: path.resolve(__dirname, './public/built'),
        filename: '[name].bundle.js',
    },

    resolve: {
        extensions: ['.tsx', '.ts', '.jsx', '.js', 'json'],
    },

    module: {
        rules: [
            {
                test: /\.(ts|tsx)$/,
                use: ['ts-loader'],
            },
            {
                test: /\.(js|jsx)$/,
                exclude: /node-modules/,
                use:{
                    loader: 'babel-loader',
                },
            },
            {
                test: /\.css$/i,
                use: ['style-loader', 'css-loader'],
            },
            {
                test: /\.(ico|gif|png|jpg|jpeg|webp)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[path][name].[ext]'
                        },
                    },
                ],
            },
            {
                test: /\.svg$/,
                use: ['@svgr/webpack', 'url-loader'],
            },
            {
                test: /\.(eot|ttf|otf)$/,
                use: 'asset/inline',
            },
        ],
    },
    plugins:[
        new HtmlWebpackPlugin({
            manifest: './public/manifest.json',
            favicon: './public/favicon.ico',
            template: './public/index.html',
        }),
    ],
    stats: 'error-only',

    devServer: {
        // historyApiFallback: true,
        // contentBase: path.resolve(__dirname, 'static'),
        historyApiFallback: {
            rewrites: [{ from: /^\/*/, to: '/index.html' }],
        },
        hot: true,
        port: 3001
    }
}