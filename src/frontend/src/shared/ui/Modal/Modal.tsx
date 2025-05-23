import type { JSX } from "react";
import ReactModal from "react-modal";

interface ModalProps {
  isOpen: boolean,
  onRequestClose?: (event: React.MouseEvent | React.KeyboardEvent) => void,
  children: JSX.Element,
}

export const Modal: React.FC<ModalProps> = ({ isOpen, onRequestClose, children }) => {
  return (
    <ReactModal
      className="flex fixed inset-0 bg-primary-ligther w-4xl mx-auto shadow rounded top-10 bottom-10"
      isOpen={isOpen}
      onRequestClose={onRequestClose}>
      {children}
    </ReactModal>
  );
}