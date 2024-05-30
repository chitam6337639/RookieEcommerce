import React from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';
import { Link } from 'react-router-dom';
import Breadcrumb from '../../components/Breadcrumbs/Breadcrumb';
import DefaultLayout from '../../layout/DefaultLayout';
import axios from 'axios';
import SelectGroupCategory from '../../components/Forms/SelectGroup/SelectGroupCategory';

interface FormValues {
  categoryName: string;
  selectedOption: string;
}

const FormCategory: React.FC = () => {
  const { register, handleSubmit, formState: { errors }, setValue, watch } = useForm<FormValues>();
  const selectedOption = watch('selectedOption', '');

  const onSubmit: SubmitHandler<FormValues> = async (data) => {
    const categoryDto = {
      categoryId: 0,
      categoryName: data.categoryName,
      parentId: data.selectedOption ? parseInt(data.selectedOption) : null,
    };

    try {
      const response = await axios.post('https://localhost:7245/api/category/create', categoryDto);
      console.log(response.data);
    } catch (error) {
      console.error('Error creating category:', error);
    }
  };

  return (
    <DefaultLayout>
      <Breadcrumb pageName="Form Category" />

      <div className="grid grid-cols-1 gap-9 sm:grid-cols-2">
        <div className="flex flex-col gap-9">
          <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
            <div className="border-b border-stroke py-4 px-6.5 dark:border-strokedark">
              <h3 className="font-medium text-black dark:text-white">
                Create Category
              </h3>
            </div>
            <form onSubmit={handleSubmit(onSubmit)}>
              <div className="flex flex-col gap-5.5 p-6.5">
                <div>
                  <label className="mb-3 block text-black dark:text-white">
                    Category Name
                  </label>
                  <input
                    type="text"
                    placeholder="Category Name"
                    {...register('categoryName', { required: 'Category name is required' })}
                    className={`w-full rounded-lg border-[1.5px] border-stroke bg-transparent py-3 px-5 text-black outline-none transition focus:border-primary active:border-primary disabled:cursor-default disabled:bg-whiter dark:border-form-strokedark dark:bg-form-input dark:text-white dark:focus:border-primary ${errors.categoryName ? 'border-red-500' : ''}`}
                  />
                  {errors.categoryName && <span className="text-red-500">{errors.categoryName.message}</span>}
                </div>

                <SelectGroupCategory
                  selectedOption={selectedOption}
                  setSelectedOption={(value: string) => setValue('selectedOption', value)}
                />

                <div className="flex justify-end gap-4 mt-6">
                  <button
                    type="submit"
                    className="inline-flex items-center justify-center bg-primary py-4 px-10 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10"
                  >
                    Submit
                  </button>
                  <Link
                    to="/"
                    className="inline-flex items-center justify-center bg-primary py-4 px-10 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10"
                  >
                    Back
                  </Link>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </DefaultLayout>
  );
};

export default FormCategory;
