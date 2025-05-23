import { useState } from "react";
import { ObservingsTable } from "./ui/ObservingsTable";
import { ObservingFormSelector } from "./ui/ObservingFormSelector";
import { CornerButton } from "../../shared/ui/CornerButton/CornerButton";
import { FaPlus } from "react-icons/fa";
import { Modal } from "../../shared/ui/Modal/Modal";

export const HomePage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);


  return <div className="m-2">
    <ObservingsTable />
    <CornerButton
      onClick={() => setIsModalOpen(true)}
      children={<FaPlus size={24} />} />
    <Modal
      isOpen={isModalOpen}
      onRequestClose={() => setIsModalOpen(false)}>
      <ObservingFormSelector isModalOpen={isModalOpen} setIsModalOpen={setIsModalOpen}></ObservingFormSelector>
    </Modal>
  </div>;
}