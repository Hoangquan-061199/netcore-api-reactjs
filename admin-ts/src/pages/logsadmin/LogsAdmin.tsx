import {
    Breadcrumb,
    Button,
    Dropdown,
    MenuProps,
    message,
    Popconfirm,
    PopconfirmProps,
    Space,
    Table,
    TableColumnsType,
    TableProps,
} from 'antd';
import { useEffect, useState } from 'react';
import { HomeOutlined, DeleteOutlined } from '@ant-design/icons';
import { useGetLogAdminQuery } from '../../redux/logsadmin/LogsAdmin.service';
import { searchModel } from '../../types/SearchModel.type';
import { LogAdmin } from '../../types/LogAdmin.type';
import { Link } from 'react-router-dom';

const LogsAdmin = () => {
    const [loading] = useState(false);
    const initArgs: searchModel = {
        keyword: '',
        sort: 0,
        page: 0,
        pageSize: 20,
    };

    const { data, isLoading } = useGetLogAdminQuery(initArgs);

    useEffect(() => {
        document.title = 'Logs Admin';
    }, []);

    console.log(data);

    const confirm: PopconfirmProps['onConfirm'] = (e) => {
        console.log(e);
        message.success('Click on Yes');
    };

    const cancel: PopconfirmProps['onCancel'] = (e) => {
        console.log(e);
        message.error('Click on No');
    };

    const confirmALL: PopconfirmProps['onConfirm'] = (e) => {
        console.log(e);
        message.success('Click on Yes all');
    };

    const cancelAll: PopconfirmProps['onCancel'] = (e) => {
        console.log(e);
        message.error('Click on No all');
    };

    const items: MenuProps['items'] = [
        {
            key: '1',
            label: (
                <Popconfirm
                    title="Xoá các logs đã chọn"
                    description={() => (
                        <p>
                            Bạn có chắc muốn xoá tất cả các logs đã chọn chứ? <br /> Xoá xong sẽ không thể khôi phục lại
                            được :)
                        </p>
                    )}
                    onConfirm={confirmALL}
                    onCancel={cancelAll}
                    okText="Yes"
                    cancelText="No">
                    Xoá tất cả
                </Popconfirm>
            ),
            icon: <DeleteOutlined />,
        },
    ];

    const columns: TableColumnsType<LogAdmin> = [
        {
            title: 'ID',
            dataIndex: 'id',
        },
        {
            title: 'Thao tác',
            dataIndex: 'action',
        },
        {
            title: 'Người dùng',
            dataIndex: 'user',
        },
        {
            title: 'Link',
            dataIndex: 'link',
        },
        {
            title: 'Ngày tạo',
            dataIndex: 'createdDate',
        },
        { title: 'Nội dung', dataIndex: 'content' },
        {
            dataIndex: 'actiontype',
            align: 'center',
            render: () => {
                return (
                    <Space>
                        <Popconfirm
                            title="Xoá Log "
                            description="Bạn chắc chắn muốn xoá log này"
                            onConfirm={confirm}
                            onCancel={cancel}
                            okText="Yes"
                            cancelText="No">
                            <Button size="small" danger={true}>
                                <DeleteOutlined />
                            </Button>
                        </Popconfirm>
                    </Space>
                );
            },
        },
    ];

    const onChange: TableProps<LogAdmin>['onChange'] = (pagination, filters, sorter, extra) => {
        console.log('params', pagination, filters, sorter, extra);
    };

    const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

    const onSelectChange = (newSelectedRowKeys: React.Key[]) => {
        console.log('selectedRowKeys changed: ', newSelectedRowKeys);
        setSelectedRowKeys(newSelectedRowKeys);
    };

    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
    };

    return (
        <div style={{ padding: 15 }}>
            <Space style={{ paddingBottom: 15 }}>
                <Dropdown menu={{ items }} placement="bottom">
                    <Button>Hành động</Button>
                </Dropdown>
                <Breadcrumb
                    items={[
                        {
                            title: <Link to={"/"}>
                            <HomeOutlined />
                            </Link>,
                        },
                        {
                            title: 'Application',
                        },
                    ]}
                />
            </Space>
            <Table
                rowSelection={rowSelection}
                columns={columns}
                dataSource={data ?? []}
                onChange={onChange}
                loading={isLoading}
                // scroll={{ y: 500 }}
                showSorterTooltip={{ target: 'sorter-icon' }}
            />
        </div>
    );
};

export default LogsAdmin;
