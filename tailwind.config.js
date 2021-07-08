module.exports = {
  mode: "jit",
  purge: ["./**/*.aspx", "./**/*.Master", "./Scripts/src/**/*.js", "./Scripts/src/**/*.vue"],
  darkMode: "class", // or 'media' or 'class'
  theme: {
      extend: {
          backgroundColor: {
              accent: {
                  DEFAULT: "#8D53C1",
                  hover: "#5E3780",
              },
          },
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
            accent: {
                DEFAULT: "#8D53C1",
                hover: "#5E3780",
            },
          },
          fontFamily: {
            garamond: ['EB Garamond']
          },
      },
  },
  variants: {
    extend: {},
  },
  plugins: [
    require('@tailwindcss/line-clamp'),
  ],
};
