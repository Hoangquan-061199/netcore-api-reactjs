import { Navigate, Outlet } from 'react-router-dom';
import { selectCurrentToken } from '../redux/auth/Auth.slice';
import { useSelector } from 'react-redux';

const _LayoutLogin = () => {
    const token = useSelector(selectCurrentToken);
    return !token ? (
        <>
            <Outlet />
        </>
    ) : (
        <Navigate to="/" replace={true} />
    );
};

export default _LayoutLogin;
