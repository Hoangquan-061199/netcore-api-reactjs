import { Modal } from 'antd';
import { ModalFooterRender } from 'antd/es/modal/interface';
import React from 'react';

interface props {
    width?: string | number;
    title: string;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleOk: () => void;
    children: React.ReactNode;
    footer?: ModalFooterRender | React.ReactNode;
}

const ModalComponent = (props: props) => {
    return (
        <>
            <Modal
            width={props.width}
                title={props.title}
                open={props.isModalOpen}
                onOk={props.handleOk}
                onCancel={props.handleCancel}
                footer={props.footer}>
                {props.children}
            </Modal>
        </>
    );
};

export default ModalComponent;
