module.exports = {
    purge: {
        enabled: true,
        content: [
            './**/*.html',
            './**/*.razor',
            './**/*.razor.css'
        ],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {},
    },
    variants: {
        extend: {},
    },
    plugins: [],
}
