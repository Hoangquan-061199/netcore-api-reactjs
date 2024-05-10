import { Route, Routes } from 'react-router-dom';
import ErrorPage from '../pages/errors/Error';

const Routers = () => {
    return (
        <>
            <Routes>
                {/* <Route path="/" element={<_Layout />}>
                    <Route index element={<Dashboard />} />
                    <Route path="/user-admin" element={<UserAdmin />} />
                </Route>
                <Route path="/login" element={<_LayoutLogin />}>
                    <Route index element={<LoginPage />} />
                </Route> */}
                <Route path="/error404" element={<ErrorPage error={'404'} subTitle={"Sorry, you are not authorized to access this page."} />} />
                <Route path="/error500" element={<ErrorPage error={'500'} subTitle={"500 Internal Server Error"} />} />
            </Routes>
        </>
    );
};

export default Routers;
