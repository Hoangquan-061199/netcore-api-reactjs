import {
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
import { useState } from 'react';
import { DeleteOutlined } from '@ant-design/icons';

interface DataType {
    id: React.Key;
    action: string;
    link: string;
    createdDate: string;
    content: string;
    user: string;
}



const LogsAdmin = () => {
    const [loading] = useState(false);
    document.title = 'Logs Admin';

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
                        <p>Bạn có chắc muốn xoá tất cả các logs đã chọn chứ? <br/> Xoá xong sẽ không thể khôi phục lại được :)</p>
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

    const columns: TableColumnsType<DataType> = [
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

    const data = [
        {
            id: 1,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 2,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 3,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 4,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 6,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
        {
            id: 5,
            action: 'Sửa',
            link: 'link',
            createdDate: '12/2/2024',
            content: 'Nội dung',
            user: 'quan',
        },
    ];

    const onChange: TableProps<DataType>['onChange'] = (pagination, filters, sorter, extra) => {
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
            </Space>
            <Table
                rowSelection={rowSelection}
                columns={columns}
                dataSource={data ?? []}
                onChange={onChange}
                loading={loading}
                // scroll={{ y: 500 }}
                showSorterTooltip={{ target: 'sorter-icon' }}
            />
        </div>
    );
};

export default LogsAdmin;
