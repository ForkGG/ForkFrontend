module.exports = {
    purge: {
        enabled: true,
        content: [
            './**/*.html',
            './**/*.razor',
            './**/*.razor.css'
        ],
        safelist: [
            'active'
        ]
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        fontSize: {
            'xs': '8pt',
            'sm': '10pt',
            'md': '12pt',
            'base': '12pt',
            'lg': '16px',
            'xl': '32px'
        },
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
            //backgroundColor: ['active'],
            //textColor: ['active']
        },
    },
    plugins: [],
}
