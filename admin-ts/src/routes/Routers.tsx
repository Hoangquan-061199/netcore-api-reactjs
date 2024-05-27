import { Route, Routes } from 'react-router-dom';
import ErrorPage from '../pages/errors/Error';
import _LayoutLogin from '../pages/_LayoutLogin';
import _Layout from '../pages/_Layout';
import Dashboard from '../pages/dashboard/Dashboard';
import LogsAdmin from '../pages/logsadmin/LogsAdmin';
import UserAdmin from '../pages/user/UserAdmin';
import Account from '../pages/user/Account';
import LoginPage from '../pages/auth/Login';
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
