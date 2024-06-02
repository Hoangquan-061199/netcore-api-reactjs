import { Route, Routes } from 'react-router-dom';
import ErrorPage from '../pages/errors/Error';
import _LayoutLogin from '../pages/_LayoutLogin';
import _Layout from '../pages/_Layout';
import Dashboard from '../pages/dashboard/Dashboard';
import LogsAdmin from '../pages/logsadmin/LogsAdmin';
import UserAdmin from '../pages/user/UserAdmin';
import Account from '../pages/user/Account';
import LoginPage from '../pages/auth/Login';
import Department from '../pages/department/Department';
import System from '../pages/systems/System';
import Resources from '../pages/systems/Resources';
import Sitemap from '../pages/systems/Sitemap';
import RedirectUrl from '../pages/systems/RedirectUrl';
import BlockIp from '../pages/systems/BlockIp';
import TemplateEmail from '../pages/systems/TemplateEmail';
import Contents from '../pages/websites/contents/Contents';
import Products from '../pages/websites/products/Products';
import ModuleContents from '../pages/websites/contents/ModuleContents';
import ModuleProducts from '../pages/websites/products/ModuleProducts';
import OtherContent from '../pages/websites/contents/OtherContent';
import Attributes from '../pages/websites/products/Attributes';
import Layouts from '../pages/layouts/Layouts';
import Menus from '../pages/menus/Menus';
import Banners from '../pages/banner/Banners';
const Routers = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<_Layout />}>
                    <Route index element={<Dashboard />} />
                    <Route path="/log-admin" element={<LogsAdmin />} />
                    {/* Authentication  */}
                    <Route path="/account-admin" element={<Account />} />
                    {/* Authentication  */}
                    <Route path="/user-admin" element={<UserAdmin />} />
                    <Route path="/department" element={<Department />} />

                    {/* websites */}
                    {/* content */}
                    <Route path="/contents" element={<Contents />} />
                    <Route path="/other-content" element={<OtherContent />} />
                    <Route path="/module-contents" element={<ModuleContents />} />
                    {/* content */}
                    {/* products */}
                    <Route path="/products" element={<Products />} />
                    <Route path="/attributes" element={<Attributes />} />
                    <Route path="/module-products" element={<ModuleProducts />} />
                    {/* products */}
                    <Route path="/layouts" element={<Layouts />} />
                    <Route path="/menus" element={<Menus />} />
                    <Route path="/banners" element={<Banners />} />

                    {/* websites */}

                    {/* systems */}
                    <Route path="/systems" element={<System />} />
                    <Route path="/resources" element={<Resources />} />
                    <Route path="/sitemap" element={<Sitemap />} />
                    <Route path="/redirect-url" element={<RedirectUrl />} />
                    <Route path="/block-ip" element={<BlockIp />} />
                    <Route path="/template-email" element={<TemplateEmail />} />
                    {/* systems */}
                </Route>
                <Route path="/login" element={<_LayoutLogin />}>
                    <Route index element={<LoginPage />} />
                </Route>
                <Route
                    path="*"
                    element={
                        <ErrorPage error={'404'} subTitle={'Sorry, you are not authorized to access this page.'} />
                    }
                />
            </Routes>
        </>
    );
};

export default Routers;
