import { Button, Result } from 'antd';
import { ResultStatusType } from 'antd/es/result';
import { Link } from 'react-router-dom';

interface Props {
    error: string;
    subTitle: React.ReactNode
}

const ErrorPage = ({ error, subTitle }: Props) => {
    return (
        <div style={{ width: '100vw', height: '100vh', display: 'flex' }}>
            <Result
                style={{ margin: 'auto' }}
                status={error as ResultStatusType}
                title={error as React.ReactNode}
                subTitle={subTitle}
                extra={
                    <Button type="primary" title="Back Home">
                        <Link to="/">Back Home</Link>
                    </Button>
                }
            />
        </div>
    );
};

export default ErrorPage;
