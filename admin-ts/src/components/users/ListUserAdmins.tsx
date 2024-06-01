import React, { useState } from 'react';
import { Avatar, Button, Dropdown, message, Popconfirm, Space, Table, Tag, Tooltip } from 'antd';
import type { MenuProps, PopconfirmProps, TableColumnsType } from 'antd';
import avatar from '../../assets/images/avatar.png';
import {
    CloseCircleOutlined,
    CheckCircleOutlined,
    LockOutlined,
    UnlockOutlined,
    CheckOutlined,
    EditOutlined,
    StopOutlined,
    DeleteOutlined,
    ClusterOutlined,
    TeamOutlined,
    PartitionOutlined,
} from '@ant-design/icons';
import ModalComponent from '../Modal';
import TreePremission from './TreePremission';
import { ModalPermissions } from '../../types/User.type';
import RolesPermission from './RolesPermission';
import FormUserAdmin from './FormUserAdmin';

interface DataType {
    key: React.Key;
    userId: string;
    userName: string;
    fullName: string;
    email: string;
    roles: string;
    roleActive: string;
    moduleAdminIds: string;
    moduleWebsiteIds: string;
    isShow: boolean;
    isActive: boolean;
    createdDate: string;
    modifiedDate: string;
    lockDate: string;
    isLock: boolean;
    departmentId: number;
    urlPicture: string;
}

const confirm: PopconfirmProps['onConfirm'] = (e) => {
    console.log(e);
    message.success('Click on Yes');
};

const cancel: PopconfirmProps['onCancel'] = (e) => {
    console.log(e);
    message.error('Click on No');
};

const items: MenuProps['items'] = [
    {
        key: '1',
        label: 'Kích hoạt tài khoản',
        icon: <CheckCircleOutlined />,
    },
    {
        key: '2',
        label: 'Huỷ kích hoạt tài khoản',
        icon: <CloseCircleOutlined />,
    },
    {
        key: '3',
        label: 'Khoá tài khoản',
        icon: <LockOutlined />,
    },
    {
        key: '4',
        label: 'Mở khoá tài khoản',
        icon: <UnlockOutlined />,
    },
    {
        key: '5',
        label: 'Ẩn tài khoản',
        icon: <StopOutlined />,
    },
    {
        key: '6',
        label: 'Hiện tài khoản',
        icon: <CheckOutlined />,
    },
    {
        key: '7',
        label: 'Xoá tài khoản',
        icon: <DeleteOutlined />,
    },
];

const data: DataType[] = [];
for (let i = 0; i < 100; i++) {
    data.push({
        key: i.toString(),
        userId: i.toString(),
        userName: '',
        urlPicture: `Edward King ${i}`,
        fullName: '',
        email: '',
        roles: '',
        roleActive: '',
        moduleAdminIds: '',
        moduleWebsiteIds: '',
        isShow: true,
        isActive: true,
        createdDate: '',
        modifiedDate: '',
        lockDate: '',
        isLock: true,
        departmentId: 1,
    });
}

// rowSelection object indicates the need for row selection
const rowSelection: any = {
    onChange: (selectedRowKeys: React.Key[], selectedRows: DataType[]) => {
        console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
    },
    getCheckboxProps: (record: DataType) => ({
        // disabled: record.userId === 'Disabled User', // Column configuration not to be checked
        name: record.key,
    }),
};

const ListUserAdmins = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [modalType, setModalType] = useState('tree');
    const [titleModal, setTitleModal] = useState('');
    const [widthModal, setWidthModal] = useState(500);
    const columns: TableColumnsType<DataType> = [
        {
            title: 'Avatar',
            dataIndex: 'urlPicture',
            width: 100,
            render: (text: string, record: DataType) => {
                return (
                    <Avatar>
                        <img src={record.urlPicture && avatar} alt="avatar" />
                    </Avatar>
                );
            },
        },
        {
            title: 'Tài khoản',
            dataIndex: 'userName',
            width: 150,
            render: (record: DataType) => {
                return (
                    <div>
                        <p>{record.userName}</p>
                        <p>{record.roles}</p>
                    </div>
                );
            },
        },
        {
            title: 'Họ tên, Email',
            dataIndex: 'email',
            render: (record: DataType) => {
                return (
                    <div>
                        <p>{record.fullName}</p>
                        <p>{record.email}</p>
                    </div>
                );
            },
        },
        {
            title: 'Trạng thái',
            dataIndex: 'isShow',
            render: (record: DataType) => {
                return (
                    <div>
                        <Tag color={true ? 'processing' : 'default'}>{true ? 'hiện' : 'ẩn'}</Tag>
                        <Tag color={true ? 'success' : 'default'}>Kích hoạt</Tag>
                        <Tag color={false ? 'error' : 'default'}>Khoá</Tag>
                    </div>
                );
            },
        },
        {
            title: 'Ngày',
            dataIndex: 'createdDate',
            render: (record: DataType) => {
                return (
                    <div>
                        <p>
                            <strong>Ngày tạo: </strong>
                            {record.createdDate}
                        </p>
                        <p>
                            <strong>Ngày sửa: </strong>
                            {record.modifiedDate}
                        </p>
                        <p>
                            <strong>Ngày khoá: </strong>
                            {record.lockDate}
                        </p>
                    </div>
                );
            },
        },
        {
            title: 'Phòng ban',
            dataIndex: 'departmentId',
            render: (record: DataType) => {
                return (
                    <div>
                        <p>{record.departmentId}</p>
                    </div>
                );
            },
        },
        {
            title: 'Phân quyền',
            dataIndex: 'Permission',
            render: () => {
                return (
                    <Space>
                        <Tooltip placement="top" title="Quản trị hệ thống">
                            <Button
                                size="small"
                                onClick={() =>
                                    showModal({
                                        type: 'tree',
                                        title: 'Phân quyền quản trị hệ thống',
                                        // userId: record.userId,
                                    })
                                }>
                                <PartitionOutlined />
                            </Button>
                        </Tooltip>
                        <Tooltip placement="top" title="Quản trị quyền">
                            <Button
                                size="small"
                                onClick={() =>
                                    showModal({
                                        type: 'role',
                                        title: 'Phân quyền quản trị quyền admin',
                                        // userId: record.userId,
                                    })
                                }>
                                <TeamOutlined />
                            </Button>
                        </Tooltip>
                        <Tooltip placement="top" title="Quản trị website">
                            <Button
                                size="small"
                                onClick={() =>
                                    showModal({
                                        type: 'tree',
                                        title: 'Phân quyền quản trị Website',
                                        // userId: record.userId,
                                    })
                                }>
                                <ClusterOutlined />
                            </Button>
                        </Tooltip>
                    </Space>
                );
            },
        },
        {
            title: 'Hành động',
            key: 'action',
            render: () => {
                return (
                    <Space>
                        <Button
                            size="small"
                            onClick={() =>
                                showModal({
                                    type: 'edit',
                                    title: 'Sửa tài khoản',
                                    width: 800
                                })
                            }
                            type="primary">
                            <EditOutlined />
                        </Button>

                        <Popconfirm
                            title="Xoá tài khoản"
                            description="Bạn có chắc muốn xoá tài khoản này?"
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

    const showModal = (props: ModalPermissions) => {
        if (props.type === 'role') setModalType('role');
        if (props.type === 'tree') setModalType('tree');
        if (props.type === 'add') setModalType('add');
        if (props.type === 'edit') setModalType('edit');
        setTitleModal(props.title);
        setWidthModal(props.width ?? 500)
        setIsModalOpen(true);
    };

    const handleOk = () => {
        setIsModalOpen(false);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };
    return (
        <>
            <ModalComponent
                width={widthModal}
                title={titleModal}
                children={
                    modalType == 'role' ? (
                        <RolesPermission />
                    ) : modalType == 'tree' ? (
                        <TreePremission />
                    ) : modalType == 'add' ? (
                        <FormUserAdmin />
                    ) : (
                        <FormUserAdmin />
                    )
                }
                isModalOpen={isModalOpen}
                handleOk={handleOk}
                handleCancel={handleCancel}
            />
            <div style={{ padding: 15 }}>
                <Space className="header-table" style={{ paddingBottom: 15 }}>
                    <Button
                        type="primary"
                        onClick={() =>
                            showModal({
                                type: 'add',
                                title: 'Thêm mới tài khoản',
                                width: 800
                            })
                        }
                        style={{ backgroundColor: '#52c41a' }}>
                        Thêm mới
                    </Button>
                    <Dropdown menu={{ items }} placement="bottomLeft" arrow>
                        <Button type="default">Hành động</Button>
                    </Dropdown>
                </Space>
                <Table
                    rowSelection={{
                        type: 'checkbox',
                        ...rowSelection,
                    }}
                    columns={columns}
                    dataSource={data}
                    pagination={{ pageSize: 20 }}
                    scroll={{ y: 'calc(65vh)' }}
                />
            </div>
        </>
    );
};

export default ListUserAdmins;
