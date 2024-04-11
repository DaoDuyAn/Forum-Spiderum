import config from '~/config';

// Layouts
import { HeaderOnly } from '~/layouts';

// Pages
import Home from '~/pages/Home';
import User from '~/pages/User'
import Login from '~/pages/Login'
import Register from '~/pages/Register'
import AuthRegister from '~/pages/AuthRegister';
import UserSettings from '~/pages/UserSettings';
import CreatePost from '~/pages/CreatePost'
import EditPost from '~/pages/EditPost'
import CategoryItem from '~/pages/CategoryItem'
import Post from '~/pages/Post'
import Search from '~/pages/Search'
import ForgotPassword from '~/pages/ForgotPassword';
import ResetPassword from '~/pages/ResetPassword';

// Public routes
const publicRoutes = [
    { path: config.routes.home, component: Home },
    { path: config.routes.user, component: User },
    { path: config.routes.login, component: Login, layout: null },
    { path: config.routes.register, component: Register, layout: null },
    { path: config.routes.auth_register, component: AuthRegister, layout: null },
    { path: config.routes.forgot_password, component: ForgotPassword, layout: null },
    { path: config.routes.reset_password, component: ResetPassword, layout: null },
    { path: config.routes.settings, component: UserSettings },
    { path: config.routes.create, component: CreatePost, layout: HeaderOnly },
    { path: config.routes.edit, component: EditPost, layout: HeaderOnly },
    { path: config.routes.category_item, component: CategoryItem },
    { path: config.routes.post, component: Post },
    { path: config.routes.search, component: Search },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };