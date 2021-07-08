module.exports = {
  mode: "jit",
  purge: ["./**/*.aspx", "./**/*.Master", "./Scripts/src/**/*.js", "./Scripts/src/**/*.vue"],
  darkMode: "class", // or 'media' or 'class'
  theme: {
<<<<<<< HEAD
      extend: {},
      fontFamily: {
          'garamond': ['Garamond']
      },
=======
    extend: {
      colors: {
        red: {
          DEFAULT: "#E60023",
          hover: "#AD081B",
        },
        light: {
          DEFAULT: "#EFEFEF",
          hover: "#E2E2E2",
        },
        dark: {
          DEFAULT: "#111111",
        },
      },
    },
>>>>>>> 18e17843a7606dc88050b9eab7d975d81e086ede
  },
  variants: {
    extend: {},
  },
  plugins: [
    require('@tailwindcss/line-clamp'),
  ],
};
