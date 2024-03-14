import config from '~/config';

// Pages
import Home from '~/pages/Home';
import User from '~/pages/User'
import Login from '~/pages/Login'
import Register from '~/pages/Register'
import UserSettings from '~/pages/UserSettings';
import CreatePost from '~/pages/CreatePost'
import EditPost from '~/pages/EditPost'
import CategoryItem from '~/pages/CategoryItem'
import Post from '~/pages/Post'
import Search from '~/pages/Search'

// Public routes
const publicRoutes = [
    { path: config.routes.home, component: Home },
    { path: config.routes.user, component: User },
    { path: config.routes.login, component: Login },
    { path: config.routes.register, component: Register },
    { path: config.routes.settings, component: UserSettings },
    { path: config.routes.create, component: CreatePost },
    { path: config.routes.edit, component: EditPost },
    { path: config.routes.category_item, component: CategoryItem },
    { path: config.routes.post, component: Post },
    { path: config.routes.search, component: Search },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };