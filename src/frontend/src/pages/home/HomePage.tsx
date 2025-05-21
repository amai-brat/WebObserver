import { useState } from "react";
import { CornerButton } from "./ui/CornerButton";
import { ObservingsTable } from "./ui/ObservingsTable";
import ReactModal from "react-modal";
import { ObservingFormSelector } from "./ui/ObservingFormWrapper";

export const HomePage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);


  return <div className="m-2">
    <ObservingsTable />
    <CornerButton onClick={() => setIsModalOpen(true)} />
    <ReactModal
      className="flex fixed inset-0 bg-primary-ligther w-4xl mx-auto shadow rounded top-10 bottom-10"
      isOpen={isModalOpen}
      onRequestClose={() => setIsModalOpen(false)}>
      <ObservingFormSelector isModalOpen={isModalOpen} setIsModalOpen={setIsModalOpen}></ObservingFormSelector>
    </ReactModal>
  </div>;
}