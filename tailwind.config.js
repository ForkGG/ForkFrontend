module.exports = {
    purge: {
        enabled: false,
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
            'xl': '20px',
            '2xl': '24px',
            '3xl': '32px'
        },
        colors: {
            transparent: 'transparent',
            current: 'currentColor',
            forkBlue: {
                dark: '#151622',
                DEFAULT: '#1D2030',
                light: '#292C3D',
                hover: '#2C3358',
                highlighted: '#374FC3',
            },
            label: {
                DEFAULT: '#575C82',
                hover: '#E2E3E9',
                selected: '#E2E3E9',
            },
            text: {
                darkest: '#1F2234',
                dark: '#575C82',
                DEFAULT: '#A3A8C1',
                red: '#CE5050',
                orange: '#CEA150',
                green: '#50CE61'
            },
            button: {
                DEFAULT: '#575C82'
            },
            status: {
                inactive: '#575C82',
                orange: '#CEA150',
                green: '#50CE61',
                red: '#CE5050'
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
