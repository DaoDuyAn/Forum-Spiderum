const routes = {
    home: '/',
    login: '/login',
    register: '/register',
    auth_register: '/auth-register',
    user: '/user/:username',
    settings: '/user/settings',
    search: '/search',
    post: '/post/:slug',
    create: '/post/create',
    edit: '/post/edit/:slug',
    category_item : '/category/:slug',
    forgot_password: '/forgot-password',
    reset_password: '/reset-password',
};

export default routes;