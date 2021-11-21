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
        colors: {
            transparent: 'transparent',
            current: 'currentColor',
            forkBlue: {
                dark: '#151622',
                DEFAULT: '#1D2030',
                light: '#292C3D'
            },
            label: {
                DEFAULT: '#626684',
                hover: '#E2E3E9',
                selected: '#E2E3E9',
            }
        },
        extend: {},
    },
    variants: {
        extend: {
            backgroundColor: ['active'],
            textColor: ['active']
        },
    },
    plugins: [],
}
