import React, { useEffect, useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import Breadcrumb from '../../components/Breadcrumbs/Breadcrumb';
import DefaultLayout from '../../layout/DefaultLayout';
import { useForm, SubmitHandler } from 'react-hook-form';
import { createProduct, updateProduct } from '../../services/product/productService';
import { getAllCategory } from '../../services/category/categoryService';
import { Category } from '../../models/Category';
import { Product } from '../../models/Product';
import { CreateProduct } from '../../models/CreateProduct';

interface FormValues {
    productName: string;
    productDescription: string;
    price: number;
    imageURL: string;
    categoryName: string;
    categoryDescription: string;
    selectedOption: string;
}

const FormProduct: React.FC = () => {
    const location = useLocation();
    const productFromState: Product = location.state;

    const { register, handleSubmit, reset, formState: { errors }, setValue } = useForm<FormValues>();
    const [categories, setCategories] = useState<Category[]>([]);
    const [selectedParentCategory, setSelectedParentCategory] = useState<Category | undefined>(undefined);
    const [selectedSubCategory, setSelectedSubCategory] = useState<number>();
    // const selectedCategoryId = selectedParentCategory?.categoryId || '';

    const queryParams = new URLSearchParams(location.search);
    const parentId = queryParams.get("parentId")

    const fetchCategories = async () => {
        try {
            const categoriesData = await getAllCategory();
            setCategories(categoriesData);
        } catch (error) {
            console.error('Error fetching categories: ', error);
        }
    };

    useEffect(() => {
        if (productFromState) {
            reset({
                productName: productFromState.productName || '',
                productDescription: productFromState.productDescription || '',
                price: productFromState.price,
                imageURL: productFromState.imageURL || '',
            });

            if (categories.length > 0) {
                var categoryOfProduct = categories.find(c => c.subCategories.some(sc => sc.categoryId == productFromState.categoryId))
                setSelectedParentCategory(categoryOfProduct);
                var subCategoryOfProduct = categoryOfProduct?.subCategories.find(sc => sc.categoryId == productFromState.categoryId);
                setSelectedSubCategory(subCategoryOfProduct?.categoryId);
                console.log(categoryOfProduct)
            }
        }
    }, [productFromState, reset, categories]);

    useEffect(() => {
        debugger
        if (categories.length == 0) {
            fetchCategories();
        }

        if (parentId && categories.length > 0) {
            var parentCategory = categories.find(c => c.categoryId == parseInt(parentId));
            setSelectedParentCategory(parentCategory);
            setValue('selectedOption', parentCategory!.categoryId.toString());
        }
    }, [categories]);

    const onSubmit: SubmitHandler<FormValues> = async (data) => {

        const productDto : CreateProduct = {
            productName: data.productName,
            productDescription: data.productDescription,
            price: data.price,
            imageURL: data.imageURL,
            categoryId: selectedSubCategory!,
            // categoryName: data.categoryName,
            // categoryDescription: data.categoryDescription,
            // parentId: data.selectedOption ? parseInt(data.selectedOption) : null,
        };
        console.log(productDto);

        // try {
        //     if (productFromState) {
        //         await updateProduct(productFromState.productId, { productName: data.productName, productDescription: data.productDescription, price: data.price, imageURL: data.imageURL });
        //         console.log('Product updated successfully');
        //     } else {
        //         const response = await createProduct(productDto);
        //         console.log(response);
        //         console.log('Category created successfully');
        //         // history.push('/path/to/redirect'); // Điều hướng sau khi tạo hoặc chỉnh sửa thành công
        //     }

        // } catch (error) {
        //     console.error('Error:', error);
        // }
        try {
            if (productFromState) {
                await updateProduct(productFromState.productId, productDto);
                console.log('Product updated successfully');
            } else {
                const response = await createProduct(productDto);
                console.log('Product created successfully', response);
            }
        } catch (error) {
            console.error('Error:', error);
        }

    };
    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const categoryId = parseInt(e.target.value)
        // setValue('selectedOption', e.target.value);
        // var parentCategory = categories.find(c => c.categoryId == parseInt(e.target.value))
        const parentCategory = categories.find(c => c.categoryId === categoryId);
        setSelectedParentCategory(parentCategory);
        // setValue('selectedCategoryId', categoryId.toString()); 
    };

    function handleSelectSubCategoryChange(event: React.ChangeEvent<HTMLSelectElement>): void {
        setSelectedSubCategory(parseInt(event.target.value));
    }

    return (
        <DefaultLayout>
            <Breadcrumb pageName="Form Category" />
            <div className="grid grid-cols-1 gap-9 sm:grid-cols-2">
                <div className="flex flex-col gap-9">
                    <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
                        <div className="border-b border-stroke py-4 px-6.5 dark:border-strokedark">
                            <h3 className="font-medium text-black dark:text-white">
                                Create Product
                            </h3>
                        </div>
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <div className="flex flex-col gap-5.5 p-6.5">
                                <div>
                                    <label className="mb-3 block text-black dark:text-white">
                                        Product Name
                                    </label>
                                    <input
                                        type="text"
                                        placeholder="Product Name"
                                        {...register('productName', { required: 'Product name is required' })}
                                        className="w-full rounded-lg border-[1.5px] border-stroke bg-transparent py-3 px-5 text-black outline-none transition focus:border-primary active:border-primary disabled:cursor-default disabled:bg-whiter dark:border-form-strokedark dark:bg-form-input dark:text-white dark:focus:border-primary"
                                    />
                                    {errors.productName && <span className="text-red-500">{errors.productName.message}</span>}
                                </div>

                                <div>
                                    <label className="mb-3 block text-black dark:text-white">
                                        Product Description
                                    </label>
                                    <input
                                        type="text"
                                        placeholder="Product Description"
                                        {...register('productDescription', { required: 'Product Description is required' })}
                                        className="w-full rounded-lg border-[1.5px] border-primary bg-transparent py-3 px-5 text-black outline-none transition focus:border-primary active:border-primary disabled:cursor-default disabled:bg-whiter dark:bg-form-input dark:text-white"
                                    />
                                    {errors.productDescription && <span className="text-red-500">{errors.productDescription.message}</span>}
                                </div>

                                <div>
                                    <label className="mb-3 block text-black dark:text-white">
                                        Price
                                    </label>
                                    <input
                                        type="text"
                                        placeholder="Price"
                                        {...register('price', { required: 'price is required' })}
                                        className="w-full rounded-lg border-[1.5px] border-primary bg-transparent py-3 px-5 text-black outline-none transition focus:border-primary active:border-primary disabled:cursor-default disabled:bg-whiter dark:bg-form-input dark:text-white"
                                    />
                                    {errors.price && <span className="text-red-500">{errors.price.message}</span>}
                                </div>
                                <div>
                                    <label className="mb-3 block text-black dark:text-white">
                                        Image URL
                                    </label>
                                    <input
                                        type="text"
                                        placeholder="Image URL"
                                        {...register('imageURL', { required: 'imageURL is required' })}
                                        className="w-full rounded-lg border-[1.5px] border-primary bg-transparent py-3 px-5 text-black outline-none transition focus:border-primary active:border-primary disabled:cursor-default disabled:bg-whiter dark:bg-form-input dark:text-white"
                                    />
                                    {errors.imageURL && <span className="text-red-500">{errors.imageURL.message}</span>}
                                </div>

                                <div className="mb-4.5">
                                    <label className="mb-2.5 block text-black dark:text-white">
                                        {' '}
                                        Category{' '}
                                    </label>

                                    <div className="relative z-20 bg-transparent dark:bg-form-input">
                                        <select
                                            value={selectedParentCategory?.categoryId}
                                            onChange={handleChange}
                                            className="relative z-20 w-full appearance-none rounded border border-stroke bg-transparent py-3 px-5 outline-none transition focus:border-primary active:border-primary dark:border-form-strokedark dark:bg-form-input dark:focus:border-primary"

                                        >
                                            <>
                                                <option value="null" className="text-body dark:text-bodydark">Select your type</option>

                                                {categories.map(category => (

                                                    <option key={category.categoryId} value={category.categoryId} className="text-body dark:text-bodydark">{category.categoryName}</option>
                                                ))}
                                            </>
                                        </select>

                                        <span className="absolute top-1/2 right-4 z-30 -translate-y-1/2">
                                            <svg
                                                className="fill-current"
                                                width="24"
                                                height="24"
                                                viewBox="0 0 24 24"
                                                fill="none"
                                                xmlns="http://www.w3.org/2000/svg"
                                            >
                                                <g opacity="0.8">
                                                    <path
                                                        fillRule="evenodd"
                                                        clipRule="evenodd"
                                                        d="M5.29289 8.29289C5.68342 7.90237 6.31658 7.90237 6.70711 8.29289L12 13.5858L17.2929 8.29289C17.6834 7.90237 18.3166 7.90237 18.7071 8.29289C19.0976 8.68342 19.0976 9.31658 18.7071 9.70711L12.7071 15.7071C12.3166 16.0976 11.6834 16.0976 11.2929 15.7071L5.29289 9.70711C4.90237 9.31658 4.90237 8.68342 5.29289 8.29289Z"
                                                        fill=""
                                                    ></path>
                                                </g>
                                            </svg>
                                        </span>
                                    </div>
                                </div>

                                <div className="mb-4.5">
                                    <label className="mb-2.5 block text-black dark:text-white">
                                        {' '}
                                        SubCategory{' '}
                                    </label>
                                    <div className="relative z-20 bg-transparent dark:bg-form-input">
                                        <select
                                            disabled={!selectedParentCategory}
                                            value={selectedSubCategory}
                                            onChange={handleSelectSubCategoryChange}
                                            className="relative z-20 w-full appearance-none rounded border border-stroke bg-transparent py-3 px-5 outline-none transition focus:border-primary active:border-primary dark:border-form-strokedark dark:bg-form-input dark:focus:border-primary"
                                        >
                                            <>
                                                <option value="null" className="text-body dark:text-bodydark">Select your category</option>
                                                {selectedParentCategory?.subCategories.map(category => (

                                                    <option key={category.categoryId} value={category.categoryId} className="text-body dark:text-bodydark">{category.categoryName}</option>
                                                ))}
                                            </>
                                        </select>

                                        <span className="absolute top-1/2 right-4 z-30 -translate-y-1/2">
                                            <svg
                                                className="fill-current"
                                                width="24"
                                                height="24"
                                                viewBox="0 0 24 24"
                                                fill="none"
                                                xmlns="http://www.w3.org/2000/svg"
                                            >
                                                <g opacity="0.8">
                                                    <path
                                                        fillRule="evenodd"
                                                        clipRule="evenodd"
                                                        d="M5.29289 8.29289C5.68342 7.90237 6.31658 7.90237 6.70711 8.29289L12 13.5858L17.2929 8.29289C17.6834 7.90237 18.3166 7.90237 18.7071 8.29289C19.0976 8.68342 19.0976 9.31658 18.7071 9.70711L12.7071 15.7071C12.3166 16.0976 11.6834 16.0976 11.2929 15.7071L5.29289 9.70711C4.90237 9.31658 4.90237 8.68342 5.29289 8.29289Z"
                                                        fill=""
                                                    ></path>
                                                </g>
                                            </svg>
                                        </span>
                                    </div>
                                </div>
                                <div className="flex justify-end gap-4 mt-6">
                                    <button
                                        type="submit"
                                        className="inline-flex items-center justify-center bg-primary py-4 px-10 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10"
                                    >
                                        Submit
                                    </button>
                                    <Link
                                        to="/tables/tableProduct"
                                        className="inline-flex items-center justify-center
                    bg-primary py-4 px-10 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10"
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

export default FormProduct;
