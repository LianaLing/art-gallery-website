module.exports = {
  mode: "jit",
    purge: [
        "./**/*.aspx",
        "./**/*.Master",
    ],
  darkMode: "class", // or 'media' or 'class'
  theme: {
      extend: {},
      fontFamily: {
          'garamond': ['Garamond']
      },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
