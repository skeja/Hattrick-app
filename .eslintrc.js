module.exports = {
  root: true,
  parserOptions: {
    parser: 'babel-eslint',
    sourceType: 'module'
  },
  // https://github.com/feross/standard/blob/master/RULES.md#javascript-standard-style
  extends: [
    'standard',
    'plugin:vue/recommended'
  ],
  // We could also use the https://github.com/vuejs/eslint-plugin-vue
  // required to lint *.vue files
  plugins: [
    'vue'
  ],
  // add your custom rules here
  'rules': {
     // allow paren-less arrow functions
     'arrow-parens': 0,

     // warn if there is a trailing comma
     'comma-dangle': [1, 'never'],

     // allow async-await
     'generator-star-spacing': 0,

     // semicolons are necessary
     'semi': ['warn', 'always'],

     // add space before function parameters
     'space-before-function-paren': ['error', {
       anonymous: 'always',
       named: 'never'
     }],
     'object-curly-spacing': ['error', 'always'],

     'vue/html-self-closing': 0,

     'vue/max-attributes-per-line': [2, {
       'singleline': 3,
       'multiline': {
         'max': 3,
         'allowFirstLine': true
       }
     }],
    // allow debugger during development
    'no-debugger': process.env.NODE_ENV === 'production' ? 2 : 0
  }
}
